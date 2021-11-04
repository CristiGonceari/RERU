import { Message } from './message';

export class Response<T = void> {
	success: boolean;
	messages: Message[];
	data: T;
}
