import { Injectable, NgZone } from '@angular/core';

declare var window: any;

@Injectable()
export class MediaCheckService {
  mqueries = {
    xsmall: '(max-width: 575px)',
    small: '(max-width: 767px)',
    medium: '(max-width: 991px)',
    large: '(max-width: 1199px)',
    xlarge: '(min-width: 1200px)'
  };

  constructor(private zone: NgZone) { }

  check(mqName: string): boolean {
    if (!this.mqueries[mqName]) {
      console.warn(`No media query registered for "${mqName}"!`);
    }
    return window.matchMedia(this.mqueries[mqName]).matches;
  }

  get getMqName(): string {
    for (const key in this.mqueries) {
      if (window.matchMedia(this.mqueries[key]).matches) {
        return key;
      }
    }
  }

  onMqChange(mqName: string, callback) {
    const self = this;

    if (typeof callback === 'function') {
      const mql = window.matchMedia(this.mqueries[mqName]);

      // if listener is already in list, this has no effect
      mql.addListener((mqlParam) => {
        self.zone.run(() => {
          if (mqlParam.matches) {
            callback(mqlParam);
          }
        });
      });

    } else {
      console.warn(`No valid callback available for "${mqName}"!`);
    }
  }
}