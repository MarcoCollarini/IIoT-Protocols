<p align="center">
  <img src="/ref/logo.png?raw=true" />
</p>

# Indice
1. [**Il Progetto**](https://github.com/MarcoCollarini/IIoT-Protocols#il-progetto)
   - [**Client**](https://github.com/MarcoCollarini/IIoT-Protocols#client)
   - [**Server**](https://github.com/MarcoCollarini/IIoT-Protocols#server)
2. [**Protocollo HTTP**](https://github.com/MarcoCollarini/IIoT-Protocols#protocollo-http)
   - [**POST-Rilevazione**](https://github.com/MarcoCollarini/IIoT-Protocols#rilevazione-post-apidetection)
   - [**POST-Monopattino**](https://github.com/MarcoCollarini/IIoT-Protocols#monopattino--post-apiscooter)
   - [**POST-Sensore**](https://github.com/MarcoCollarini/IIoT-Protocols#sensore--post-apisensor)
   - [**Response**](https://github.com/MarcoCollarini/IIoT-Protocols#response)
3. [**Protocollo MQTT**](https://github.com/MarcoCollarini/IIoT-Protocols#protocollo-mqtt)
   - [**Gestione messaggi da client a server**](https://github.com/MarcoCollarini/IIoT-Protocols#gestione-messaggi-da-client-a-server)
   - [**Gestione messaggi da server a client**](https://github.com/MarcoCollarini/IIoT-Protocols#gestione-messaggi-da-server-a-client)
   - [**Test**](https://github.com/MarcoCollarini/IIoT-Protocols#test)
4. [**Protocollo AMQP**](https://github.com/MarcoCollarini/IIoT-Protocols#protocollo-amqp)
   - [**Gestione messaggi da client a server**](https://github.com/MarcoCollarini/IIoT-Protocols#gestione-messaggi-da-client-a-server-1)
5. [**Il team**](https://github.com/MarcoCollarini/IIoT-Protocols#il-team)

# Il progetto
Si sviuppi un'applicativo che permetta lo scambio di dati tra sensori (*client*) e un server. 

Si richiede inoltre, che la comunicazione tra i dispositivi, avvenga tramite l'utilizzo di diversi protocolli, riusciendo ad adattare il codice nel migliore dei modi. 

### Client
- Creazione sensori virtuali
  - Sensore di batteria
  - Sensore di velocità
  - Sensore di posizione
  - Sensore di stato
  
- Invio delle rilevazioni al server

### Server
- Ricezione ed invio di dati
- Gestione dati con salvataggio nel database 

# Protocollo HTTP

<p align="center">
  <img src="/ref/httpSchema.png?raw=true" />
</p>

### Rilevazione (`POST /api/detection`)

**Formato JSON**

| **Nome variabile**      | **Tipo variabile**        |
|-------------------------|---------------------------|
| SensorId                | int                       |
| ScooterId               | int                       |
| SensorValue             | string                    |
| SensorType              | string                    |
| SensorDetectionDate     | DateTime                  |


### Monopattino  (`POST /api/scooter`)

**Formato JSON**

| **Nome variabile**      | **Tipo variabile**        |
|-------------------------|---------------------------|
| ScooterId               | int                       |
| Brand                   | string                    |
| Company                 | string                    |


### Sensore  (`POST /api/sensor`)

**Formato JSON**

| **Nome variabile**      | **Tipo variabile**        |
|-------------------------|---------------------------|
| SensorId                | int                       |
| SensorType              | string                    |
| SensorMesurementUnit    | string                    |


### Response 
**Formato JSON**

| **Nome variabile**      | **Tipo variabile**        |
|-------------------------|---------------------------|
| StatusCode              | int                       |
| Message                 | string                    |


# Protocollo MQTT

<p align="center">
  <img src="/ref/mqttSchema.PNG?raw=true" />
</p>

### Gestione messaggi da client a server
- Il server utilizza il subscribe al topic **scooter/#**
- Il client utilizza il publish al topic **scooter/*ScooterId*/*SensorId*/*SensorType***

**BODY Messaggio**

| **Nome variabile**      | **Tipo variabile**        |
|-------------------------|---------------------------|
| SensorValue             | string                    |
| SensorDetectionDate     | DateTime                  | 

### Gestione messaggi da server a client
- Il server utilizza il publish al topic **scooter/*ScooterId*/*SensorId*/*SensorType***
- Il client utilizza il subscribe al topic **sensor/*SensorId*/status**

**BODY Messaggio**

| **Nome variabile**      | **Tipo variabile**        |
|-------------------------|---------------------------|
| Status                  | bool                      |

## **TEST**

- Server → publish su topic **sensor/1/status**
- Client → subscribe su topic **sensor/1/status**
 
 ***NOTE:***
 
  Tramite questa funzionalità è possibile andare ad attivare/disattivare un determinato sensore.

# Protocollo AMQP

<p align="center">
  <img src="/ref/amqpSchema.PNG?raw=true" />
</p>

### Gestione messaggi da client a server
- Ogni sensore invia i dati ad una coda `Redis` accessibile a tutti i sensori presenti nel monopattino
- Un ulteriore processo (*producer*) va a controllare se nella coda sono presenti messaggi
  - Nel caso in cui ci siano dei messaggi nella coda viene stabilita una connessione al broker AMQP e vengono inviati i dati all'exchanger
- L'exchanger, impostato in modo da filtrare i messaggi attraverso la `routeKey`, inserisce il messaggio in una determinata coda
- Il server (*consumer*) va a leggere i dati da una determinata coda, salvandoli successivamente nel DB

**BODY Messaggio**

| **Nome variabile**      | **Tipo variabile**        |
|-------------------------|---------------------------|
| SensorId                | int                       |
| ScooterId               | int                       |
| SensorValue             | string                    |
| SensorType              | string                    |
| SensorDetectionDate     | DateTime                  |

Sono state create **4 code**:
- *Battery_Sensor* → coda che contiene tutte le rilevazioni del sensore della batteria
- *Speed_Sensor* → coda che contiene tutte le rilevazioni del sensore della velocità
- *Movement_Sensor* → coda che contiene tutte le rilevazioni del sensore del movimento
- *Position_Sensor* → coda che contiene tutte le rilevazioni del sensore di posizione

> NB: Questa soluzione è stata implementata solo per testare le funzionalità dell'exchanger

# Il team

- [**Marco Collarini**](https://github.com/MarcoCollarini) → *progettazione e sviluppo software lato **server***
- [**Alessandro Vendrame**](https://github.com/alessandrovendrame) → *progettazione e sviluppo software lato **client***










