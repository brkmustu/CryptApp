import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

class cryptHub {
  constructor() {
    this.client = new HubConnectionBuilder()
      .withUrl("http://crypt.eventprocessor:5000/hubs/crypt", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .build();
  }
  start() {
    this.client.start();
  }
}

export const getCryptHub = () => {
  return new cryptHub();
};
