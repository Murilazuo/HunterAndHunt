# HunterAndHunt
// descrição

# Conceitos

# Implementação
Diagrama UML

![Diagrama UML do projeto](https://user-images.githubusercontent.com/78811958/233791387-626d3c42-90a8-4f3f-9cca-9edd681ee958.jpg)

## Menagers 

### Game Manager
  Responsável por instanciar os agentes e controlar o game loop do jogo.
  
#### Variáveis
  - GameObject[] agentPrefabs : prefabs dos agentes que serão instanciados
  - List<Agent> agents = new() : lista de agentes presentes na cena
  - int minPrey : minimo de presas a serem instanciadas
  - int maxPrey; : máximo de presas a serem instanciadas
  - float stepTime : Delay entre cada chamada do agente.
  - int turnCount = 0 : número atual de turnos
    
  + static GameManager instance (Singleton)
  
#### Métodos
  - Start : Instancia os objetos na cena
  - GameObject SpawnAgentInRandomPosition(AgentType agentType) : Gera uma posição vazia dentro do gride chama SpawnAgent(Agent)
  - GameObject SpawnAgent(Vextor2Int pos, AgentType) : Instancia o Agente e adiciona sua posição ao Grid
  + void RemoveAgent(Agent agent) : Remove o agente da lista de agentes na cenaAdiciona 
  + void NextTurn() : Incrementa turnCount e chama a routine Turn
  - IEnumerator Turn() : Chama TakeAction de cada agente presente na cena
  + void Reload() : Recarrega a cena
  
### Grid
  Responável por criar o grid que armazena as posições dos Agents do jogo.
  

## Agents
  

### Agent
  Classe abstrata para os agentes do jogo.
  
### Hunter
  Agent que caça os Agents Preys. Possui dois estados, Hunt e Move.
 
  Move: O Agente se movimenta aleatoriamente em uma das oito direções até que esteja próximo de uma Prey, ai passa para o estado Hunt.
  Hunt: O Agente se movimenta na direção da Prey mais próxima dele.
### Prey
  Agente que foge dos Hunters. Possui dois estados Escape e Hunt.
  Move: Possui o mesmo comportamento 
## Utils

### SetSpeed
Altera a velocidade da simulação.
