declare module 'simple-peer' {
  interface Options {
    initiator?: boolean;
    stream?: MediaStream;
    trickle?: boolean;
  }

  interface SignalData {
    type: string;
    sdp: string;
  }

  export default class SimplePeer {
    constructor(options?: Options);

    signal(data: string | SignalData): void;
    on(event: string, callback: (data: any) => void): void;
    addStream(stream: MediaStream): void;
    destroy(): void;
  }
}
