import { IInputs, IOutputs } from "./generated/ManifestTypes";

export class NumberedListComponent implements ComponentFramework.StandardControl<IInputs, IOutputs> {
    private _container: HTMLDivElement;

    constructor() {
    }

    public init(context: ComponentFramework.Context<IInputs>, notifyOutputChanged: () => void, state: ComponentFramework.Dictionary, container: HTMLDivElement) {
        const style = document.createElement("style");
        style.innerHTML = `
            .numberedList {
                text-align: left;
            }
        `;
        document.head.appendChild(style);
        this._container = document.createElement("div");
        this._container.contentEditable = "true";
        this._container.className = "numberedList";
        this._container.innerHTML = `
            <ol>
                <li>Item 1</li>
                <li>Item 2</li>
            </ol>`;
        container.appendChild(this._container);

        this._container.addEventListener("keydown", this.handleKeyDown.bind(this));
    }

    private handleKeyDown(event: KeyboardEvent): void {
        if (event.key === "Enter") {
            document.execCommand("insertHTML", false, "<li><br></li>");
            event.preventDefault();
        }
    }

    public updateView(context: ComponentFramework.Context<IInputs>): void {
    }

    public getOutputs(): IOutputs {
        return {};
    }

    public destroy(): void {
    }
}
