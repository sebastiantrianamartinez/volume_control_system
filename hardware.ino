#define TRIG_PIN 7
#define ECHO_PIN 6

void setup() {
  Serial.begin(9600); // Inicializa la comunicación serial para mostrar resultados
  pinMode(TRIG_PIN, OUTPUT);
  pinMode(ECHO_PIN, INPUT);
}

void loop() {
  long duration, distance;
  
  // Envía un pulso corto al pin TRIG para activar el sensor
  digitalWrite(TRIG_PIN, LOW);
  delayMicroseconds(2);
  digitalWrite(TRIG_PIN, HIGH);
  delayMicroseconds(10);
  digitalWrite(TRIG_PIN, LOW);
  
  // Lee la duración del pulso de eco desde el pin ECHO
  duration = pulseIn(ECHO_PIN, HIGH);
  
  // Calcula la distancia en centímetros usando la fórmula d = (t * v) / 2, donde t es el tiempo en microsegundos y v es la velocidad del sonido en el aire (aprox. 343 m/s)
  distance = (duration * 0.0343) / 2;
  
  // Muestra la distancia en el monitor serial
  Serial.print(distance);
  Serial.print('\n');
  
  delay(500); // Espera un segundo antes de tomar otra lectura
}
