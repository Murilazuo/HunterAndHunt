# HunterAndHunt
// descrição

# Conceitos

# Implementação
Diagrama UML

![Diagrama UML do projeto](https://user-images.githubusercontent.com/78811958/233791387-626d3c42-90a8-4f3f-9cca-9edd681ee958.jpg)

## Menagers 

### Game Manager
  Responsável por instanciar os agentes e controlar o game loop do jogo.
  
  
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
