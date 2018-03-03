export class WorkTypeHours{
    value: number;
    key: string;
    label: string;
    required: boolean;

    constructor(options: {
        value?: number,
        key? : string,
        label?: string,
        required? : boolean
    } = {}) {
        this.value = options.value;
        this.key = options.key || '';
        this.label = options.label || '';
        this.required = !!options.required;
    }
}