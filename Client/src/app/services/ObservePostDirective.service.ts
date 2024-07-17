import { Directive, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';

@Directive({
  selector: '[appObservePost]'
})
export class ObservePostDirective implements OnInit, OnDestroy {
  @Input() postId: string = "";
  @Output() visible = new EventEmitter<string>();
  @Output() hidden = new EventEmitter<string>();

  private observer!: IntersectionObserver;

  constructor(private el: ElementRef) { }

  ngOnInit() {
    this.observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          this.visible.emit(this.postId);
        } else {
          this.hidden.emit(this.postId);
        }
      });
    });

    this.observer.observe(this.el.nativeElement);
  }

  ngOnDestroy() {
    this.observer.disconnect();
  }
}
