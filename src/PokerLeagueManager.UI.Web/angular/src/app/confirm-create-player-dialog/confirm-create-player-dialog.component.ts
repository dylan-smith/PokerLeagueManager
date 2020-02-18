import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'confirm-create-player-dialog',
  templateUrl: 'confirm-create-player-dialog.component.html',
})
export class ConfirmCreatePlayerDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ConfirmCreatePlayerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
}