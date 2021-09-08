export interface Show {
    id: number;
    title: string;
    description: string;
    releaseDate: Date;
    coverImageUrl: string;
    showType: string;
    actors: string[];
    averageRating: number;
}