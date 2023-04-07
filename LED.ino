int data;

void setup() {
Serial.begin (9600);
pinMode(10,OUTPUT);
pinMode(12,OUTPUT);  


}

void loop() {
 if (Serial.available())
 {
  data = Serial.read();
  if (data == 'A') //Mendapatkan perintah A dari game
  {
    digitalWrite (10, HIGH);
    digitalWrite (12, LOW);
    delay(2000); //PINTU TERBUKA
    
    digitalWrite (10, LOW);
    digitalWrite (12, LOW);
    delay(4000); // DELAY
    digitalWrite (12, HIGH);
    
    digitalWrite (10,LOW);
   
    delay(2000);
    digitalWrite (10, LOW);
    digitalWrite (12, LOW);//PINTU MENUTUP
  }
 }

}
