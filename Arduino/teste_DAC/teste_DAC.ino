char buf[3] = {0};
uint8_t length = 3;
long tempo = 0;
uint16_t dac = 0;

void setup()
{
    Serial.begin(115200);
    pinMode(A0, INPUT);
    analogReadResolution(12);
    analogWriteResolution(12);
}

void loop()
{
    /*
    if(Serial.available())
    {
        uint16_t valorDAC;
        if(Serial.readBytesUntil('\n', buf, length))
        {
            valorDAC = atoi(buf);
            analogWrite(DAC0, valorDAC);
        }
        for (int i = 0; i < 3; i++)
        {
            Serial.print(buf[i]);
        }
        Serial.println();
        
    }*/

    if(millis() - tempo > 5)
    {
        tempo = millis();
        analogWrite(DAC0, dac);
        dac += 1;
        delay(1);
        Serial.print(dac);
        Serial.print('\t');
        Serial.println(analogRead(A0));
        if(dac >= 4096)
            dac = 0;
    }
}
