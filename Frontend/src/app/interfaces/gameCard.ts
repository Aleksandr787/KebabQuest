export interface IGameCard {
  id?: string;
  image: string;
  title: string;
}

export interface IGameStory {
  id: string;
  plot: string;
  image: string;
  question: string;
  title: string;
  options: {
    [key: string]: string
  }
}

export interface IGameNextStep {
  question: string;
  image: string;
  answer: string;
  options: {
    [key: string]: string
  }
}
