export class SelectItem {
    label: string;
    value: string;

    public constructor(init?:Partial<SelectItem>) {
        Object.assign(this, init);
    }
}