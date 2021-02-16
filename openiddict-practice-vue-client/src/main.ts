import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { getUser } from "@/services/AuthService";

getUser().then(() => {
  createApp(App)
    .use(router)
    .mount("#app");
});
