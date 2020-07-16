#include "Eletroestimulador.h"

Eletroestimulador::Eletroestimulador(uint8_t _pino_dac)
{
    pino_dac = _pino_dac;
}

void Eletroestimulador::checkSerial(estados *estadoAtual)
{
    while(Serial.available() > 2)
    {
        uint8_t buf_length = 0;
        char codigo[4];
        char valor_buf[32] = {'f'};
        uint32_t valor = 0;
        uint8_t buf[BUF_LENGTH] = {'f'};

        // Lê até a quebra de linha ('\n')
        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH);

        // Sai do loop caso a leitura tenha tamanho 0 (para evitar quaisquer erros de leitura)
        if(buf_length == 0)
            break;

        // Faz uma string com os 3 primeiros caracteres e salva em 'cod'
        for(int i = 0; i < 3; i++) codigo[i] = buf[i];
        codigo[3] = '\0';
        String cod = String(codigo);

        // Caso o comando tenha mais que 3 posições, o traço é ignorado e os valores posteriores são gravados em valor_buf
        if(buf_length > 3)
        {
            for(int i = 4; i < buf_length; i++) valor_buf[i - 4] = buf[i];
            valor = atoi(valor_buf);    // Converte valor em inteiro
        }
        
        
        // Compara qual código que foi recebido (switch-case não funciona com string em C++)
        if(cod.equals(String("STA")))
        {
            switch(onda)
            {
                case QUADRADA:
                    *estadoAtual = EE_SQUARE;
                    break;
                
                case SPIKE:
                    *estadoAtual = EE_SPK;
                    break;

                default:
                    *estadoAtual = EE_WF;
                    break;
            }
        }
            
        else if(cod.equals(String("STO")))
        {
            //state_changed = 1;
            *estadoAtual = STAND_BY;
        }

        // Define a corrente de saída e já define o valor de saída do DAC em bits
        else if(cod.equals(String("IAM")))
        {
            if(valor < 360) valor = 360;
            if(valor > 1823) valor = 1823;
            i_amp = valor;  // Valor da corrente em uA

            // tensão = 1k5 * corrente
            //float tensao = 1500 * (i_amp * 0.000001); // Converte de uA pra A
            //tensao *= 100; // Transforma de 1,5 pra 150
            //valor_DAC = map((uint16_t)tensao, 54, 274, 0, 4095);    // Converte pra saida do DAC em 8 bits de resolução
            valor_DAC = map(i_amp, 360, 1823, 0, 127);
            //Serial.println(valor_DAC);

            //analogWrite(SAIDA_DAC, saida_DAC);

            // 1/6*V ~ 5/6*V | V = 3.3V
            // 0,54 até 2,74
            // 360 uA até 1823 uA
        }

        // Define a frequência da onda e já calcula seu período em us
        else if(cod.equals(String("FRQ")))
        {
            freq = valor;
            period = (uint32_t)(1 / (double)freq);   // Adquire o período em s
            period *= 1000000;  // Transforma pra us
            //period = (uint32_t)(100000000.0 / (double)freq);    // Transforma pra unidades de 0,01 ns
        }

        // Define a lagura de pulso da onda em us
        else if(cod.equals(String("BDW")))
        {
            bandwidth = valor;
        }

        // Ajusta o sistema para gerar determinada onda
       else if (cod.equals(String("WFM")))
       {
            for(int i = 0; i < 3; i++) codigo[i] = valor_buf[i];
            cod = String(codigo);

            if(cod.equals(String("SQR")))
            {
                onda = QUADRADA;
                setupOndaQuad();
            }

            if(cod.equals(String("SPK")))
            {
                onda = SPIKE;
                //setupOndaQuad();
            }

            if(cod.equals(String("SIN")))
            {
                onda = SENOIDE;
                //setupOndaQuad();
            }

            if(cod.equals(String("TRI")))
            {
                onda = TRIANGULAR;
                //setupOndaQuad();
            }

            if(cod.equals(String("DTS")))
            {
                onda = DENTESERRA;
                //setupOndaQuad();
            }
       }

        else if(cod.equals(String("TDR")))
        {
            total_duration = valor;
        }

        else if(cod.equals(String("REP")))
        {
            //printReport();
        }
        else if(cod.equals(String("ATT")))
        {
            //printReport_Simple();
        }
        else if(cod.equals(String("RND")))
        {
            /*
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

            }*/           
        }

        else if (cod.equals(String("IDR")))
       {
            for(int i = 0; i < 3; i++) codigo[i] = valor_buf[i];
            cod = String(codigo);

            if(cod.equals(String("ANO")))
            {
                direcao_corrente = ANODICA;
            }

            if(cod.equals(String("CAT")))
            {
                direcao_corrente = CATODICA;
            }

            if(cod.equals(String("BID")))
            {
                direcao_corrente = BIDIRECIONAL;
            }
       }
    }
}

void Eletroestimulador::checkSerial_Fast(estados *estadoAtual)
{
    while(Serial.available() > 2)
    {
        uint8_t buf_length = 0;
        uint8_t buf[BUF_LENGTH_SMALL] = {'f'};

        // Lê até a quebra de linha ('\n')
        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH_SMALL);

        // Sai do loop caso a leitura tenha tamanho 0 (para evitar quaisquer erros de leitura)
        if(buf_length == 0 || buf_length > 3) // Sai do loop caso seja maior que 3 (o terminador é descartado)
            break;

        // Se a mensagem for 'STO', ele volta pro STAND_BY
        if( (buf[0] == 'S') && (buf[1] == 'T') && (buf[2] == 'O'))
            *estadoAtual = STAND_BY;

        break;
    }
}

// Função chamada na maquina de estados
void Eletroestimulador::geraOndaQuad(estados *estadoAtual)
{
    long tempo_on_total = millis();
    long tempo_step = 0;
    uint8_t valor_saida = 127;
    uint8_t multiplier = 1;
    uint8_t sum = 0;

    // Ajusta a direção de saída de corrente
    switch (direcao_corrente)
    {
        // Catodica: 0 a 1.65 V
        case CATODICA:
            sum = 0;
            multiplier = 1;
            break;

        // Anodica 1.65 a 3.3 V
        case ANODICA:
            sum = 128;
            multiplier = 1;
            break;

        // Bidirecional 0 a 3.3 V
        case BIDIRECIONAL:
            sum = 0;
            multiplier = 2;
            break;
    }
    

    // Só sai do while se receber o comando STO ou atingir o tempo máximo
    while (true)
    {
        for(int i = 0; i < SQUARE_WAVE_RES; i++) // Controla a saída da onda
        {
            valor_saida = (valor_DAC * multiplier) + sum;
            dacWrite(pino_dac, valor_saida); // Controla a saída do DAC de acordo com a onda guardada em ondaQ
            tempo_step = micros();

            // Mantém a saída do DAC de acordo com o tempo entre cada amostra
            while(micros() - tempo_step < step);
            //delayMicroseconds(step); 
        }

        // Checa o estado da serial a cada ciclo da onda
        checkSerial_Fast(estadoAtual);  // Verifica se o comando STO não chegou

        // Caso o estado mude, ele sai da função
        if(*estadoAtual != EE_SQUARE)
        {
            dacWrite(pino_dac, 128);
            return;
        }
        // Sai da função se atingir o tempo máximo
        if( ((millis() - tempo_on_total) > total_duration) && (total_duration != 0))
        {
            *estadoAtual = STAND_BY;
            return;
        }
    }
    //ledcWrite(CANAL_PWM, dutyCycle);
}

// Função chamada quando chega a mensagem 'WFM-SQR'
// Regula as variáveis para que a função geraOndaQuad funcione corretamente
void Eletroestimulador::setupOndaQuad()
{
    uint16_t samples;
    
    step = period / SQUARE_WAVE_RES;  // Tempo entre uma amostra e outra
    samples = uint16_t(bandwidth / step);     // Quantidade de amostras no tempo de bandwidth

    // Proteção caso o número de amostras ultrapasse a quantidade máxima
    if(samples > SQUARE_WAVE_RES)
        samples = SQUARE_WAVE_RES;
    
    // Forma a onda quadrada de acordo com a largura de pulso escolhida
    for(int i = 0; i < samples; i++)
        ondaQ[i] = 1;
    if(samples != SQUARE_WAVE_RES)
    {
        for(int i = samples; i < SQUARE_WAVE_RES; i++)
            ondaQ[i] = 0;
    }


    //ledcSetup(CANAL_PWM, freq, resolution);

    //ledcAttachPin(PINO_SAIDA, CANAL_PWM);
}

void Eletroestimulador::geraFormaDeOnda()
{
    // ver qual a onda ativa
}