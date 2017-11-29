import { Component, Input } from '@angular/core';

@Component({
    selector: 'ant-panel',
    templateUrl: './panel.component.html'
})
export class PanelComponent {
    @Input()
    public title = "Panel"
}
