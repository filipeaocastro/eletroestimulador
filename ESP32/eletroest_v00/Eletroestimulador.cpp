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
        char valor_buf[BUF_LENGTH] = {'f'};
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
                    timer_on = false;
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
            if(valor < 0) valor = 0;
            if(valor > 5000) valor = 5000;
            i_amp = valor;  // Valor da corrente em uA

            // tensão = 1k5 * corrente
            //float tensao = 1500 * (i_amp * 0.000001); // Converte de uA pra A
            //tensao *= 100; // Transforma de 1,5 pra 150
            //valor_DAC = map((uint16_t)tensao, 54, 274, 0, 4095);    // Converte pra saida do DAC em 8 bits de resolução
            valor_DAC = map(i_amp, 0, 5000, 0, 127);
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
            period = (uint32_t)(1000000 / freq);   // Adquire o período em us
            //period *= 1000000;  // Transforma pra us
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
                setupOndaQuad();    // Define os parâmetros para a geração de onda quadrada
                Serial.write("OK!\n");  // Confirma para o sistema que os dados estão atualizados
            }

            if(cod.equals(String("SPK")))
            {
                onda = SPIKE;
                setupSpike();
                Serial.write("OK!\n");  // Confirma para o sistema que os dados estão atualizados
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

       else if (cod.equals(String("BGN")))
       {
           Serial.write("Recebeu BGN");
           Serial.println(valor);
           uint16_t index_sum = 0;
           total_spikes = valor;
           while(true)
           {
               if(Serial.available() > 1)
               {
                    buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH);

                    Serial.write("buf_length = ");
                    Serial.println(buf_length);
                    //Serial.println(buf);


                    if((buf[0] == 'E') && (buf[1] == 'N') && (buf[2] == 'D'))
                        break;

                    for(int i = 0; i < buf_length; i++)
                    {
                        spike_data[index_sum + i] = (buf[i] == '1');
                    }
                    index_sum += buf_length;
               }
               
           }
           /*
           Serial.write("Saiu do while");
           Serial.println();
           uint8_t sum = 0;
           for(uint16_t i = 0; i < total_spikes; i++)
           {
                if(spike_data[i])
                    Serial.write("1");
                else
                    Serial.write("0");
                
                sum ++;
                if(sum >= 100)
                {
                    Serial.println();
                    sum = 0;
                }
           }*/
            
       }
    }
}

void Eletroestimulador::checkSerial_Fast(estados *estadoAtual)
{
    while(Serial.available() > 2)
    {
        uint8_t buf_length = 0;
        uint8_t buf[BUF_LENGTH_SMALL] = {'f'};
        //Serial.write("Checou\n");

        // Lê até a quebra de linha ('\n')
        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH_SMALL);

        // Sai do loop caso a leitura tenha tamanho 0 (para evitar quaisquer erros de leitura)
        if(buf_length == 0) // Sai do loop caso seja maior que 3 (o terminador é descartado)
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
    long tempo_teste = millis();
    uint32_t tempo_step = 0;
    uint8_t valor_saida = 127;
    uint8_t multiplier = 1;
    uint8_t sum = 0;
    uint8_t valorUp = 0;
    uint8_t valorDown = 0;

    Serial.write("INITIATED\n");

    // Ajusta a direção de saída de corrente
    switch (direcao_corrente)
    {
        // Catodica: 0 a 1.65 V
        case CATODICA:
            //sum = 0;
            //multiplier = 1;
            valorUp = 127 - valor_DAC;
            valorDown = 127;
            break;

        // Anodica 1.65 a 3.3 V
        case ANODICA:
            //sum = 128;
            //multiplier = 1;
            valorUp = 128 + valor_DAC;
            valorDown = 127;
            break;

        // Bidirecional 0 a 3.3 V
        case BIDIRECIONAL:
            sum = 0;
            multiplier = 2;
            valorUp = 128 + valor_DAC;
            valorDown = 127 - valor_DAC;
            break;
    }

    //uint8_t conta = (valor_DAC + sum) * multiplier;
    if(valorUp > 255) valorUp = 255;
    if(valorDown > 255) valorDown = 255;

    if(valorUp < 0) valorUp = 0;
    if(valorDown < 0) valorDown = 0;
    
    uint16_t newStep = (uint16_t)step;
    

    /************* timer **************/
    uint32_t tempoTotal = total_duration * 10000;    // Converte o total duration pra us pra comparar com o que o timer incrementa

    Serial.write("tempoTotal = ");
    Serial.println(tempoTotal);

    portENTER_CRITICAL(&timerMux);
    indexWave = 0;
    countTotal = 0;
    portEXIT_CRITICAL(&timerMux);

    if(ondaQ[indexWave] == 1)
    {
        portENTER_CRITICAL(&timerMux);
        DAC_out =  valorUp; // Controla a saída do DAC de acordo com a onda guardada em ondaQ
        portEXIT_CRITICAL(&timerMux);
    }    
    else
    {
        portENTER_CRITICAL(&timerMux);
        DAC_out =  valorDown; // Controla a saída do DAC de acordo com a onda guardada em ondaQ
        portEXIT_CRITICAL(&timerMux); 
    }
    timerAlarmWrite(timer_INT, nTicks, true);
    timerAlarmEnable(timer_INT);
    
    

    


    // Só sai do while se receber o comando STO ou atingir o tempo máximo
    while (true)
    {
        /*
        for(uint16_t i = 0; i < SQUARE_WAVE_RES; i++) // Controla a saída da onda
        {
            //Serial.println(i);
            //valor_saida = ondaQ[i] * conta;
            
            if(ondaQ[i] == 1)
                dacWrite(pino_dac, valorUp); // Controla a saída do DAC de acordo com a onda guardada em ondaQ
            else
            {
                dacWrite(pino_dac, valorDown); // Controla a saída do DAC de acordo com a onda guardada em ondaQ
            }


            //dacWrite(pino_dac, valor_saida); // Controla a saída do DAC de acordo com a onda guardada em ondaQ
            tempo_step = micros();

            // Mantém a saída do DAC de acordo com o tempo entre cada amostra
            while(micros() - tempo_step < newStep);
            //delayMicroseconds(newStep); 
        }*/

        // Checa o estado da serial a cada ciclo da onda
        //checkSerial_Fast(estadoAtual);  // Verifica se o comando STO não chegou
        /*
        if(*estadoAtual == EE_SQUARE)
            Serial.write("Ta em EE_SQUARE\n");

        else
            Serial.write("Nao ta em EE_SQUARE\n");
            */
        // Caso o estado mude, ele sai da função
        /*
        if(*estadoAtual != EE_SQUARE)
        {
            dacWrite(pino_dac, 128);
            Serial.write("STOPPED\n");
            return;
        }*/
        // Sai da função se atingir o tempo máximo
        /*
        if( ((millis() - tempo_on_total) > total_duration) && (total_duration != 0))
        {
            *estadoAtual = STAND_BY;
            Serial.write("STOPPED\n");
            return;
        }*/

        // ******************* CONTROLE POR TIMER **********************
        // Não precisa usar o critical section pra ler variável compartilhada

        if(interrompeu)
        {
            dacWrite(pino_dac, DAC_out);
            portENTER_CRITICAL(&timerMux);
            interrompeu = false;
            portEXIT_CRITICAL(&timerMux);
            
            countTotal += (uint32_t)nTicks;
            if(indexWave == SQUARE_WAVE_RES)
                indexWave = 0;
            
            else
                indexWave++;
        }


        if(ondaQ[indexWave] == 1)
        {
            //portENTER_CRITICAL(&timerMux);
            DAC_out =  valorUp; // Controla a saída do DAC de acordo com a onda guardada em ondaQ
            //portEXIT_CRITICAL(&timerMux);
        }    
        else
        {
            //portENTER_CRITICAL(&timerMux);
            DAC_out =  valorDown; // Controla a saída do DAC de acordo com a onda guardada em ondaQ
            //portEXIT_CRITICAL(&timerMux); 
        }

        if(*estadoAtual != EE_SQUARE)
        {
            timerAlarmDisable(timer_INT);
            dacWrite(pino_dac, 128);
            Serial.write("STOPPED\n");
            return;
        }

        if(countTotal >= tempoTotal)
        {
            timerAlarmDisable(timer_INT);
            *estadoAtual = STAND_BY;
            Serial.write("STOPPED\n");
            return;
        }
        checkSerial_Fast(estadoAtual);  // Verifica se o comando STO não chegou

/*
        if((millis() - tempo_teste) > 1000)
        {
            tempo_teste = millis();
            Serial.print("countTotal = ");
            Serial.write(countTotal);
            Serial.println();
        }
*/
    }
}

// Função chamada quando chega a mensagem 'WFM-SQR'
// Regula as variáveis para que a função geraOndaQuad funcione corretamente
void Eletroestimulador::setupOndaQuad()
{
    uint16_t samples;


    
    step = float(period) / float(SQUARE_WAVE_RES);  // Tempo entre uma amostra e outra
    if (step <= 0)
        step = 0.1;
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
    
    Serial.write("step = ");
    Serial.println(step);
    Serial.write("period = ");
    Serial.println(period);
    Serial.write("samples = ");
    Serial.println(samples);

    for(int i = 0; i < SQUARE_WAVE_RES; i++)
    {
        if(ondaQ[i] == 0)
            Serial.write("_");
        else
            Serial.write("-");
    }
    Serial.println();

    //********** LIGANDO O TIMER **********
    portENTER_CRITICAL(&timerMux);
    nTicks = (uint64_t)(step * 10);
    portEXIT_CRITICAL(&timerMux);

    Serial.write("nTicks = ");
    Serial.println((uint32_t)nTicks);

    //ledcSetup(CANAL_PWM, freq, resolution);

    //ledcAttachPin(PINO_SAIDA, CANAL_PWM);
}

void Eletroestimulador::geraFormaDeOnda()
{
    // ver qual a onda ativa
}

void Eletroestimulador::IRQtimer()
{
    /*
    dacWrite(pino_dac, DAC_out);
    portENTER_CRITICAL_ISR(&timerMux);
    countTotal += nTicks;
    portEXIT_CRITICAL_ISR(&timerMux);
    if(indexWave == SQUARE_WAVE_RES)
    {
        portENTER_CRITICAL_ISR(&timerMux);
        indexWave = 0;
        portEXIT_CRITICAL_ISR(&timerMux);
    }
    else
    {
        portENTER_CRITICAL_ISR(&timerMux);
        indexWave++;
        portEXIT_CRITICAL_ISR(&timerMux);
    } */
    portENTER_CRITICAL_ISR(&timerMux);
    interrompeu = true;
    portEXIT_CRITICAL_ISR(&timerMux);

    // botar o timer mux em cima pra ver se faz diferença

}

void Eletroestimulador::configTimer(hw_timer_t * _timer)
{
    //timerAttachInterrupt(_timer, fn, true);
    timer_INT = _timer;
}

void Eletroestimulador::geraSpike(estados *estadoAtual)
{
    bool nextSpike = false;
    uint16_t spkIndex = 0;
    bool spike_off = true;
    uint8_t spk_true = 0;

    if(timer_on == false)
    {
        timer_on = true;
        Serial.write("INITIATED\n");
        timerAlarmWrite(timer_INT, nTicks, true);
        timerAlarmEnable(timer_INT);
    }
    /*
    while(Serial.available() > 2)
    {
        uint8_t buf_length = 0;
        
        //Serial.write("Checou\n");

        // Lê até a quebra de linha ('\n')
        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf_spk, 3);
        

        // Sai do loop caso a leitura tenha tamanho 0 (para evitar quaisquer erros de leitura)
        if(buf_length == 0) // Sai do loop caso seja maior que 3 (o terminador é descartado)
            break;

            // Colocar pra mandar tamanho do buffer

        // Se a mensagem for 'STO', ele volta pro STAND_BY
        if(buf_spk[0] == '0')
            dacWrite(pino_dac, spk_off);
        else if(buf_spk[0] == '1')
            dacWrite(pino_dac, spk_on);
        else
        {
            dacWrite(pino_dac, 128);
            *estadoAtual = STAND_BY;
            Serial.write("STOPPED\n");
            Serial.write("Buffer length: ");
            Serial.println(buf_length);
            for(int i = 0; i < buf_length; i++)
            {
                Serial.write(buf_spk[i]);
                Serial.println();   
            }
                
        }
            

        break;
    }*/

    while(true)
    {
        if(interrompeu)
        {
            portENTER_CRITICAL(&timerMux);
            interrompeu = false;
            portEXIT_CRITICAL(&timerMux);
            spkIndex ++;
            nextSpike = true;
            if(spk_true > 0)
                spk_true --;
        }

        if(spkIndex > total_spikes)
            spkIndex = 0;

    
        if(nextSpike)
        {
            nextSpike = false;

            if(spike_data[spkIndex])
            {
                //Serial.write("SPK\n");
                dacWrite(pino_dac, spk_on);
                spk_true = 3;   // Mantém o spike ligado por 3 ms
                spike_off = false;
            }
            else if(!spike_data[spkIndex] && (spk_true == 0) && (spike_off == false) )
            {
                dacWrite(pino_dac, spk_off);
                spike_off = true;
            }
        }
        

        checkSerial_Fast(estadoAtual);  // Verifica se o comando STO não chegou
        if(*estadoAtual != EE_SPK)
        {
            timerAlarmDisable(timer_INT);
            dacWrite(pino_dac, 128);
            Serial.write("STOPPED\n");
            timer_on = false;
            return;
        }
    }
}
void Eletroestimulador::setupSpike()
{
    switch (direcao_corrente)
    {
        // Catodica: 0 a 1.65 V
        case CATODICA:
            //sum = 0;
            //multiplier = 1;
            spk_on = 127 - valor_DAC;
            spk_off = 127;
            break;

        // Anodica 1.65 a 3.3 V
        case ANODICA:
            //sum = 128;
            //multiplier = 1;
            spk_on = 128 + valor_DAC;
            spk_off = 127;
            break;

        // Bidirecional 0 a 3.3 V
        
        case BIDIRECIONAL:
        /*
            sum = 0;
            multiplier = 2;
            valorUp = 128 + valor_DAC;
            valorDown = 127 - valor_DAC;*/
            break;
            
    }

    nTicks = (uint64_t)(10000);    // Cada tick = 0.1 us
    // 10000 * 0.1 us = 1 ms

    Serial.write("nTicks = ");
    Serial.println((uint32_t)nTicks);
}

void Eletroestimulador::interromp(volatile bool * _int, portMUX_TYPE * _timerMux)
{
    //interrompeu = _int;
    //timerMux = _timerMux;
}

/*
    Estimulação nao para quando mandar parar
*/