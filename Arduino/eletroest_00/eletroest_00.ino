/**
Software de controle do eletroestimulador
Autor: Filipe Augusto
março/2020

O controle do eletroestimulador será feito por um Arduino DUE através de uma saída PWM com frequência e largura de pulso variáveis e uma 
saída analógica (DAC) para controle da amplitude da corrente aplicada. Uma interface em C# ficará conectada ao Arduino através de comunicação
serial e irá definir os parâmetros da eletroestimulação, sendo estes:
    NOME                            VARIÁVEL                PROTOCOLO       UNIDADE
    Amplitude da corrente           (i_amp)                 IAM-            uA
    Frequência da corrente          (freq)                  FRQ-            Hz
    Largura de pulso                (bandwidth)             BDW-            %
    Duração de cada burst           (burst_width)           BRW-            us
    Intervalo entre bursts          (burst_interval)        BRI-            us
    Duração de um trem de bursts    (burst_train_width)     BTW-            ms
    Intervalo entre trens bursts    (burst_train_interval)  BTI-            ms
    Duração total da estimulação    (total_duration)        TDR-            ms


**/
#include "pwm_lib.h"

#define BUF_LENGTH 64

using namespace arduino_due::pwm_lib;

// Variáveis definidas pelo usuário
uint32_t i_amp = 500;                //uA               
uint32_t freq = 1000;                //Hz
uint32_t bandwidth = 50;             //%
uint32_t burst_width = 100;          //us
uint32_t burst_interval = 100;       //us
uint32_t burst_train_width = 10;     //ms
uint32_t burst_train_interval = 10;  //ms
uint32_t total_duration = 500;       //ms


uint32_t period = 1000000; // 10ms (em 0,01 ns)
uint32_t duty = 0;  // Deve ser transformado em unidades de 0,01 ns também

uint8_t buf[BUF_LENGTH] = {0};

long time_past = 0;

pwm<pwm_pin::PWMH1_PA19> pwm_pin42;  //Pino 42

void setup()
{
    Serial.begin(115200);
    pinMode(42, OUTPUT);    // Ver se isso não atrapalha
    duty = (unsigned long) map(bandwidth, 0, 100, 0, period);
    pwm_pin42.start(period, duty);
}
void loop()
{

    while(Serial.available())
    {
        uint8_t buf_length;
        char codigo[4];
        char valor_buf[32];
        uint32_t valor;

        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH);

        if(buf_length == 0)
            break;

        for(int i = 0; i < 3; i++) codigo[i] = buf[i];
        codigo[3] = '\0';
        String cod = String(codigo);

        for(int i = 3; i < buf_length; i++) valor_buf[i - 3] = buf[i];
        valor = atoi(valor_buf);
        


        // Compara qual código que foi recebido (switch-case não funciona com string)
        if(cod.equals(String("IAM")))
        {
            i_amp = valor;
        }

        else if(cod.equals(String("FRQ")))
        {
            freq = valor;
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
        }

        else if(cod.equals(String("TDR")))
        {
            total_duration = valor;
        }
        
    }


    if(millis() - time_past > 5000)
    {
        time_past = millis();
        bandwidth += 10;
        
        period /= 2;
        duty = (unsigned long) map(bandwidth, 0, 100, 0, period);

        pwm_pin42.set_period_and_duty(period, duty);
        
        if(bandwidth >= 100)  bandwidth = 0;
    }
}
