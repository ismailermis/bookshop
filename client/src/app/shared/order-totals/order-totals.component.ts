import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent {
  // @Input() shippingPrice: number;
  // @Input() subtotal: number;
  // @Input() total: number;

  constructor(public basketService:BasketService) { }

}
