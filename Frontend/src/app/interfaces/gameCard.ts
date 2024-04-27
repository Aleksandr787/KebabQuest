export interface IGameCard {
  image: string;
  title: string;
}

export interface GameStory {
  id: string;
  plot: string;
  image: string;
  question: string;
  title: string;
  options: {
    [key: string]: string
  }
}
