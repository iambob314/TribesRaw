
// %heap.size
// %heap.elem[%i] (1 based)
function Heap::new() {
  %heap = newObject("Heap", SimSet);
  %heap.size = 0;
  return %heap;
}

function Heap::size(%heap) {
  return %heap.size;
}

function Heap::add(%heap, %value) {
  %heap.elem[%heap.size++] = %value;

  for (%i = %heap.size; %i > 1; %i >>= 1) {
    if ((%heap.elem[%i]|0) < (%heap.elem[%i >> 1]|0)) {
      %t = %heap.elem[%i];
      %heap.elem[%i] = %heap.elem[%i >> 1];
      %heap.elem[%i >> 1] = %t;
    }
  }
}

function Heap::removeMin(%heap) {
  if (%heap.size == 0) return "";

  %min = %heap.elem[1];
  %heap.elem[1] = %heap.elem[%heap.size];
  %heap.size--;

  for (%i = 1; (%i<<1) <= %heap.size; 1) {
    if ((%i<<1)+1 <= %heap.size)
      %minChild = tern( (%heap.elem[(%i<<1)]|0) < (%heap.elem[(%i<<1)+1]|0), (%i<<1), (%i<<1)+1);
    else
      %minChild = (%i<<1);

    if ((%heap.elem[%i]|0) > (%heap.elem[%minChild]|0)) {
      %t = %heap.elem[%i];
      %heap.elem[%i] = %heap.elem[%minChild];
      %heap.elem[%minChild] = %t;

      %i = %minChild;
    } else break;
  }

  return %min;
}

function Heap::echoHeap(%heap) {
  echo("Heap:");
  for (%i = 1; %i <= %heap.size; %i++) {
    echo(%i @ ":" @ %heap.elem[%i]);
  }
}