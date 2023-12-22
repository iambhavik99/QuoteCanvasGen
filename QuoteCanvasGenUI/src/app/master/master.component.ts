import { Component, OnInit } from '@angular/core';
import { ApiService } from '../service/api.service';
import { QuoteImageResponse } from '../common/models/QuoteImageResponse.model';

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.css']
})
export class MasterComponent implements OnInit {

  quoteImageResponse!: QuoteImageResponse;
  imageUrl!: string;

  STATES = { LOADING: 'LOADING', SUCCESS: 'SUCCESS', FAILED: 'FAILED' }
  currentState = this.STATES.LOADING;

  constructor(
    private apiService: ApiService
  ) {
    this.quoteImageResponse = new QuoteImageResponse();
  }

  ngOnInit(): void {
    this.fetchQuoteImageWithMetaData();
  }


  fetchQuoteImageWithMetaData() {

    this.currentState = this.STATES.LOADING;

    this.apiService.get("api/generate")
      .toPromise()
      .then(response => {
        this.quoteImageResponse = response;
        this.imageUrl = `${this.quoteImageResponse.imageUrl}?q=${Date.now()}`;

        this.currentState = this.STATES.SUCCESS;

      })
      .catch(err => {
        this.currentState = this.STATES.FAILED;
        console.error(err);
      })
  }


}
