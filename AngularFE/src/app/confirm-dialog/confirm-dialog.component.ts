import { Component, OnInit, Inject } from '@angular/core';
import {  MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent {
  title: string;
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData) {
      this.title = data.title;
    }

    close(): void {
      this.dialogRef.close(false);
    }

    confirm(): void {
      this.dialogRef.close(true);
    }
}

export interface ConfirmDialogData {
  title: string;
}
