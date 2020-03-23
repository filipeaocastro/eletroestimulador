/**
Software de controle do eletroestimulador
Autor: Filipe Augusto
março/2020

O controle do eletroestimulador será feito por um Arduino DUE através de uma saída PWM com frequência e largura de pulso variáveis e uma 
saída analógica (DAC) para controle da amplitude da corrente aplicada. Uma interface em C# ficará conectada ao Arduino através de comunicação
serial e irá definir os parâmetros da eletroestimulação, sendo estes:
    NOME                            VARIÁVEL                PROTOCOLO       UNIDADE
    Amplitude da corrente           (i_amp)                 IAM             uA
    Frequência da corrente          (freq)                  FRQ             Hz
    Largura de pulso                (bandwidth)             BDW             %
    Duração de cada burst           (burst_width)           BRW             us
    Intervalo entre bursts          (burst_interval)        BRI             us
    Duração de um trem de bursts    (burst_train_width)     BTW             ms
    Intervalo entre trens bursts    (burst_train_interval)  BTI             ms
    Duração total da estimulação    (total_duration)        TDR             ms

    Outros protocolos:
    STA                     Start - Inicia a estimulação
    STO                     Stop - Interrompe a estimulação
    REP                     Report - Mostra todas as variáveis
    MSG                     Messages - Ativa ou desativa as mensagens de início/fim

**/
#include "pwm_lib.h"

#define BUF_LENGTH 64
#define SAIDA_DAC DAC0
using namespace arduino_due::pwm_lib;

// Variáveis definidas pelo usuário
uint32_t i_amp = 500;                //uA               
uint32_t freq = 100000;                //Hz
uint32_t bandwidth = 75;              //%
uint32_t burst_width = 50000;          //us
uint32_t burst_interval = 10000;       //us
uint32_t burst_train_width = 1000;     //ms
uint32_t burst_train_interval = 1000;  //ms
uint32_t total_duration = 10000;       //ms
uint32_t random_bti_min = 500;       //ms
uint32_t random_bti_max = 3000;      //ms


uint32_t period = 100000; // (em 0,01 ns)
uint32_t duty = 0;  // Deve ser transformado em unidades de 0,01 ns também
uint32_t burst_train_interval_static = 1000;  //ms

uint16_t valor_DAC = 0;

bool burst_on = 0;
bool train_burst_on = 0;
bool estimulation_on = 0;
bool alert_msg_on = 1;
bool state_changed = 0;
bool random_bti_on = false;

unsigned long total_time_past = 0;
unsigned long train_time = 0;

pwm<pwm_pin::PWMH1_PA19> pwm_pin42;  //Pino 42

void setup()
{
    analogWriteResolution(12);
    Serial.begin(115200);
    pinMode(42, OUTPUT);    // Ver se isso não atrapalha
    duty = (unsigned long) map(bandwidth, 0, 100, 0, period);
    pwm_pin42.start(period, duty);
    randomSeed(analogRead(5));
}
void loop()
{

    while(Serial.available() > 2)
    {
        uint8_t buf_length = 0;
        char codigo[4];
        char valor_buf[32] = {'f'};
        uint32_t valor = 0;
        uint8_t buf[BUF_LENGTH] = {'f'};

        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH);

        if(buf_length == 0)
            break;

        for(int i = 0; i < 3; i++) codigo[i] = buf[i];
        codigo[3] = '\0';
        String cod = String(codigo);

        if(buf_length > 3)
        {
            for(int i = 3; i < buf_length; i++) valor_buf[i - 3] = buf[i];
            valor = atoi(valor_buf);
        }
        
        
        // Compara qual código que foi recebido (switch-case não funciona com string em C++)
        if(cod.equals(String("STA")))
        {
            
            inicia();

        }
            
        else if(cod.equals(String("STO")))
        {
            state_changed = 1;
            stop();
        }
        else if(cod.equals(String("IAM")))
        {
            if(valor < 360) valor = 360;
            if(valor > 1823) valor = 1823;
            i_amp = valor;  // Valor da corrente em uA

            // tensão = 1k5 * corrente
            //float tensao = 1500 * (i_amp * 0.000001); // Converte de uA pra A
            //tensao *= 100; // Transforma de 1,5 pra 150
            //valor_DAC = map((uint16_t)tensao, 54, 274, 0, 4095);    // Converte pra saida do DAC em 12 bits de resolução
            valor_DAC = map(i_amp, 360, 1823, 0, 4095);
            //Serial.println(valor_DAC);

            //analogWrite(SAIDA_DAC, saida_DAC);

            // 1/6*V ~ 5/6*V | V = 3.3V
            // 0,54 até 2,74
            // 360 uA até 1823 uA
        }

        else if(cod.equals(String("FRQ")))
        {
            freq = valor;
            period = (uint32_t)(100000000.0 / (double)freq);    // Transforma pra unidades de 0,01 ns
        }

        else if(cod.equals(String("BDW")))
        {
            bandwidth = valor;
        }

        else if(cod.equals(String("BRW")))
        {
            burst_width = valor;
        }
        
        else if(cod.equals(String("BRI")))
        {
            burst_interval = valor;
        }

        else if(cod.equals(String("BTW")))
        {
            burst_train_width = valor;
        }

        else if(cod.equals(String("BTI")))
        {
            burst_train_interval = valor;
            burst_train_interval_static = valor;
        }

        else if(cod.equals(String("TDR")))
        {
            total_duration = valor;
        }

        else if(cod.equals(String("REP")))
        {
            printReport();
        }
        else if(cod.equals(String("MSG")))
        {
            if(valor > 0)
                alert_msg_on = true;
            else
                alert_msg_on = false;
        }
        else if(cod.equals(String("RND")))
        {
            if(buf_length < 6)
                break;

            //char codigo_rnd[4];
            for(int i = 0; i < 3; i++) codigo[i] = valor_buf[i];
            cod = String(codigo);

            if(cod.equals(String("OFF")))
            {
                random_bti_on = false;
                burst_train_interval = burst_train_interval_static;
            }
                
            else
            {
                if(buf_length <= 6)
                    break;

                char rnd_valor_buf[32];
                for(unsigned int i = 3; i < sizeof(valor_buf); i++) rnd_valor_buf[i - 3] = valor_buf[i];
                valor = atoi(rnd_valor_buf);

                if(cod.equals(String("MAX")))
                {
                    random_bti_on = true;
                    random_bti_max = valor;
                }
                else if(cod.equals(String("MIN")))
                {
                    random_bti_on = true;
                    random_bti_min = valor;
                }

            }           
        }
    }
    // Verifica se já passou o tempo total e guarda o resultado em 'on_time_past'
    bool on_time_past = (millis() - total_time_past) <= total_duration;
    if(on_time_past && estimulation_on)
    {
        if(train_burst_on)
        {
            if(burst_on)
            {
                pwm_pin42.start(period, duty);
                delayMicroseconds(burst_width);
                pwm_pin42.stop(); 
                digitalWrite(42, LOW);
                burst_on = !burst_on;
            }
            else
            {        
                delayMicroseconds(burst_interval);
                burst_on = !burst_on;
            }

            if((millis() - train_time) >= burst_train_width)
            {
                pwm_pin42.stop();
                digitalWrite(42, LOW); 
                train_burst_on = !train_burst_on;
                train_time = millis();
            }
        }
        else
        {
            if((millis() - train_time) >= burst_train_interval)
            {
                train_burst_on = !train_burst_on;
                train_time = millis();
                if(random_bti_on)
                    burst_train_interval = (uint32_t)random(random_bti_min, random_bti_max);
            }
        }
        state_changed = 1;
    }
    else if(!on_time_past || !estimulation_on)
        stop();
    
}

void inicia()
{
    if(alert_msg_on)
        Serial.println("Inicio da estimulação");
    analogWrite(SAIDA_DAC, valor_DAC);

    if(random_bti_on)
        burst_train_interval = (uint32_t)random(random_bti_min, random_bti_max);

    duty = (unsigned long) map(bandwidth, 0, 100, 0, period);
    estimulation_on = true;
    train_burst_on = true;
    burst_on = true;
    total_time_past = millis();
    train_time = millis();
    state_changed = 1;
}
void stop()
{
    if(state_changed)
    {
        estimulation_on = false;
        pwm_pin42.stop(); 
        if(alert_msg_on)
            Serial.println("Fim da estimulacao");
        state_changed = 0;
        analogWrite(SAIDA_DAC, 0);
    }
    
}
void printReport()
{
    Serial.println("\n********************************");
    Serial.println("PARAMETROS DA ELETROESTIMULAÇÃO:\n");

    Serial.print("IAM = ");
    Serial.print(i_amp);
    Serial.println(" uA");

    Serial.print("FRQ = ");
    Serial.print(freq);
    Serial.println(" Hz");

    Serial.print("BDW = ");
    Serial.print(bandwidth);
    Serial.println(" %");

    Serial.print("BRW = ");
    Serial.print(burst_width);
    Serial.println(" us");

    Serial.print("BRI = ");
    Serial.print(burst_interval);
    Serial.println(" us");

    Serial.print("BTW = ");
    Serial.print(burst_train_width);
    Serial.println(" ms");

    Serial.print("BTI = ");
    Serial.print(burst_train_interval);
    Serial.println(" ms");

    Serial.print("TDR = ");
    Serial.print(total_duration);
    Serial.println(" ms");

    Serial.print("RND = ");
    Serial.print(random_bti_on);
    Serial.println(" ON/OFF");

    Serial.print("RNDMAX = ");
    Serial.print(random_bti_max);
    Serial.println(" ms");

    Serial.print("RNDMIN = ");
    Serial.print(random_bti_min);
    Serial.println(" ms");
}
