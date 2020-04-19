import { ICountry } from './country';

export interface IFood {
    id: number;
    name: string;
    imageUrl: string;
    description: string;
    popularInList: ICountry[];
    addedByName: string;
    addedAt: Date;
}
