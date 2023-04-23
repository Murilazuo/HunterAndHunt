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
  - float stepTime : Delay entre cada chamada do agente
  - int turnCount = 0 : número atual de turnos
    
  - public static GameManager instance (Singleton-  )
  
#### Métodos
  - Start : Instancia os objetos na cena
  - GameObject SpawnAgentInRandomPosition(AgentType agentType) : Gera uma posição vazia dentro do gride chama SpawnAgent(Agent)
  - GameObject SpawnAgent(Vextor2Int pos, AgentType) : Instancia o Agente e adiciona sua posição ao Grid
  - public void RemoveAgent(Agent agent) : Remove o agente da lista de agentes na cena 
  - public void NextTurn() : Incrementa turnCount e chama a routine Turn
  -  IEnumerator Turn() : Chama TakeAction de cada agente presente na cena
  - public void Reload() : Recarrega a cena
  
### Grid
  Responável por criar o grid que armazena as posições dos Agents do jogo.
  
#### Variáveis 
  - GameObject gridCell : Preab de celula do grid, apenas estético.
  - Vector2Int gridSize : Tamanho do grid
  - Agent[,] cells : Array 2D que armazena a posição dos Agentes no jogo

  - public static Grid Instance (Singleton)

#### Métodos
  - Awake() : cria o array de cells e instancia um gridCell para cada posição do grid
  - public Agent GetCell(Vector2Int pos) : retorna o agente na posição 
  - public void SetAgent(Vector2Int pos, Agent value) : define o Agente na posição
  - public void DestroyCell(Vector2Int pos) : Chama DestroyAgente do Agente e define a posição como nula
  - public bool InBounds(VectorInt pos) : Renorna verdadeiro se a posição estiver dentro do grid
  - public int MaxCells() : Retorna a múltiplicação do altura pela largura do grid
  - public Vector2Int RandomPositionInBounds() : Retorna uma posição aleatória dentro do grid

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
