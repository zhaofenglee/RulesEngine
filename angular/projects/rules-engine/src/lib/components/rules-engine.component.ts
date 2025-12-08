import { Component, inject, OnInit } from '@angular/core';
import { RulesEngineService } from '../services/rules-engine.service';

@Component({
  selector: 'lib-rules-engine',
  template: ` <p>rules-engine works!</p> `,
  styles: [],
})
export class RulesEngineComponent implements OnInit {
  private service = inject(RulesEngineService);

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
