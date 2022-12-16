import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import PrimeVue from "primevue/config";
import Button from "primevue/button";
import Card from "primevue/card";
import Toast from "primevue/toast";
import ToastService from "primevue/toastservice";
import Password from "primevue/password";
import Checkbox from "primevue/checkbox";
import InputText from "primevue/inputtext";
import Textarea from "primevue/textarea";
import { getCryptHub } from "./hubs/crypt.js";
import { appAxios } from "./utils/appAxios.js";

import "@/assets/styles.scss";

import "primevue/resources/themes/saga-blue/theme.css";
import "primevue/resources/primevue.min.css";
import "primeicons/primeicons.css";

const app = createApp(App);
app.use(router);
app.use(store);
app.use(PrimeVue);
app.use(ToastService);

app.component("Button", Button);
app.component("Card", Card);
app.component("Checkbox", Checkbox);
app.component("Password", Password);
app.component("InputText", InputText);
app.component("Toast", Toast);
app.component("Textarea", Textarea);
app.provide("appAxios", appAxios);
app.provide("getCryptHub", getCryptHub);

app.mount("#app");
