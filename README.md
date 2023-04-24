# HunterAndHunt
Desenvolvido por Murilo Bezerra e Lucas Eilert

# Conceitos

Simulação de agentes com comportamentos distintos em um grid limitado. Existem dois tipos de agentes, as presas (Prey) e os caçadores (Hunter). Normalmente, ambos andam aleatoriamente em uma das oito direções, quando o Hunter fica a uma certa distância de uma Prey, ele vai em sua direção, e a Prey vai na direção oposta do Hunter. 

# Implementação
Diagrama UML

![Diagrama UML do projeto](https://user-images.githubusercontent.com/78811958/234140001-511030b6-7ff6-4956-8217-df80c5f3f4ed.png)

## Menagers 

### Game Manager
  Responsável por instanciar os agentes e controlar o game loop do jogo. Possui uma lista com todos os agentes na cena, a cada turno, o GameManager chama TakeAction de todos esses agentes.
  
#### Variáveis
  - GameObject[] agentPrefabs : prefabs dos agentes que serão instanciados
  - List<Agent> agents = new() : lista de agentes presentes na cena
  - int minPrey : minimo de presas a serem instanciadas
  - int maxPrey; : máximo de presas a serem instanciadas
  - float stepTime : Delay entre cada chamada do agente
  - int turnCount = 0 : número atual de turnos
    
  - public static GameManager instance : Singleton
  
#### Métodos
  - Start : Instancia os objetos na cena
  - GameObject SpawnAgentInRandomPosition(AgentType agentType) : Gera uma posição vazia dentro do gride chama SpawnAgent(Agent)
  - GameObject SpawnAgent(Vextor2Int pos, AgentType) : Instancia o Agente e adiciona sua posição ao Grid
  - public void RemoveAgent(Agent agent) : Remove o agente da lista de agentes na cena 
  - public void NextTurn() : Incrementa turnCount e chama a routine Turn
  -  IEnumerator Turn() : Chama TakeAction de cada agente presente na cena
  - public void Reload() : Recarrega a cena
  
### Grid
  Responável por criar o grid que armazena as posições dos Agents do jogo. Por meio de uma array 2D de agentes, é possível verificar a posição dos agentes na cena, usando a posição do mundo como index. Caso uma celula retorne null, a célula está vazia, se aposição a ser consultada for menor que zero e o valor máximo de ambas as coordenadas, a posição está fora dos limites do grid.
  
#### Variáveis 
  - GameObject gridCell : Preab de celula do grid, apenas estético.
  - Vector2Int gridSize : Tamanho do grid
  - Agent[,] cells : Array 2D que armazena a posição dos Agentes no jogo

  - public static Grid Instance : Singleton

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

#### Variáveis 
  - enum EnemyType { Hunter, Prey }
  - float timeToMove : tempo que o agente leva para se movimentar.
  - Vector2Int Pos : propriedade que retorna a posição atual em Vector2Int
  - delegate void State() : métodos delegate que serão utilizados para aticar os States dos Agentes
  - State states[] : lista de estados que será inicializada nas classes concretas
  - State currentState : estado atual de acordo com o enum de states das classes concretas, é definida no SetState()

#### Métodos 
  - abstract void TakeAction() : é chamado pelo GameManager quando roda um novo turno
  - virtual vois DestroyAgent() : Remove o agente do grid é do GameManager e se destroy
  - void Move(Vector2Int dir) : Se move na direção e atualiza sua posição no grid
  - void MoveInRandomDirection() : Se move aleatoriamente em direção a uma posição que esteja dentro do grid (Grid.InBounds) e que não esteja ocupando por nenhum outro agente (Grid.GetCell(Pos + dir) == null).
  - void SetState(int stateId) : Defini cuurentState de acordo com states, utilizando stateId como index


### Hunter
  ![Finit Machine State de Hunter](https://user-images.githubusercontent.com/78811958/234137442-3bd5ea3f-d004-454d-ae16-7df567eca941.png)

  Agent que caça os Agents Preys. Possui dois estados, Hunt e Walk.
 
  Walk: O Agente se movimenta aleatoriamente em uma das oito direções até que esteja próximo de uma Prey, ai passa para o estado Hunt.
  Hunt: O Agente se movimenta na direção da Prey mais próxima dele.
  
#### Viáveis
  - enum HunterState { Walk, Hunt }
  - List<Prey> preyAround : lista de inimigos na visionRange do Hunter no turno atual.
  - int visionRange : Range de visão 
  - int attackRange : Range que o Hunter irá atacar
  - public static Hunter Instance : Singleton

  
#### Métodos
  - void TakeAction() : popula a lista preyAround com GetPreyAround() e invoca currentState.
  
  - void WalkState() : Elemento 0 do array states. Método invocado por currentState, Se HasPreyAround() for verdadeiro, chama SetState(HuntState.Hunt), se não chama MoveInRandomDirection()
  - void HuntState() : Elemento 1 do array states.Método invocado por currentState, Se HasPReyAround() for verdadeiro, Se HasPreyInAttackRange() for verdadeiro, chame KillAgent() da Prey mais próxima, se não vai em direção a Prey mais próxima, se não SetState(HuntState.Walk)
  
  - void GoInPreyDirection() : Vai em direção a Prey mais próxima.
  - bool HasPreyArround() : Retorna verdadeiro se o count de preyAround > 0.
  - bool HasPreyInAttackRange() : Retorna verdadeiro se houver ao menous uma Prey na área de ataque de Hunt.
  - Prey GetCloserPrey() : Retorna o Prey mais próximo.
  - void KillPrey(Prey prey) : Chama KillAgent de prey.
  - void GetPreyAtound() : Adiciona os prey que estão no range de visionRange a preyAround.
  
### Prey
  ![Finit Machine State de Prey](https://user-images.githubusercontent.com/78811958/234137483-33c9fa54-ef51-43c9-9a40-7b879b565836.png)

  Agente que foge dos Hunters. Possui dois estados Escape e Walk.
  Walk: Possui o mesmo comportamento de Walk Hunt.
  
#### Variáveis
  - enum PreyState { Walk, Escape } 
  - int distanceToRun
 
#### Métodos
  - void TakeAction() : invoca currentState.
  
  - void WalkState() : Elemento 0 do array states. Método invocado por currentState, Se HunterIsInDistanceToRun() for verdadeiro, chama SetState(PreyState.Escape), se não chama MoveInRandomDirection()
  - void EscapeState() : Elemento 1 do array states. Método invocado por currentState, Se HunterIsInDistanceToRun() for verdadeiro, chama GoInOpositeHunterDirection(), se não SetState(PreyState.Walk)
  
  - bool HunterIsInDistanceToRun() : retorna verdadeiro se a distancia de Hunter for menor que distanceToRun
  - void GoInOpositeHunterDirection() : vai na direção oposta do Hunter.
  
## Utils

### SetSpeed
Altera a velocidade da simulação.
