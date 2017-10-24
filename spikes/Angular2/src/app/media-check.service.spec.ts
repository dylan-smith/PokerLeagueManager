import { TestBed, inject } from '@angular/core/testing';

import { MediaCheckService } from './media-check.service';

describe('MediaCheckService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MediaCheckService]
    });
  });

  it('should be created', inject([MediaCheckService], (service: MediaCheckService) => {
    expect(service).toBeTruthy();
  }));
});
