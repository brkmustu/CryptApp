<script setup>
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import { useToast } from "primevue/usetoast";
import { ref, inject } from "vue";
import { setJwtTokenHeader } from "@/utils/appAxios.js";

const apiKey = ref("");
const password = ref("");
const appAxios = inject("appAxios");

const router = useRouter();
const store = useStore();
const toast = useToast();

const onSubmit = () => {
  if (!apiKey.value || !password.value) {
    toast.add({
      severity: "warning",
      summary: "Uyarı!",
      detail: "Lütfen ilgili alanları doldurun!",
      life: 5000,
    });
    return;
  }

  appAxios
    .post("auth/signin", {
      ApiKey: apiKey.value,
      Password: password.value,
    })
    .then((response) => {
      if (response && response.data && response.data.data) {
        toast.add({
          severity: "success",
          summary: "İşlem başarılı!",
          detail: "Oturum açma işlemi başarılı.",
          life: 3000,
        });

        store.commit("setSignInInfo", {
          apiKey: apiKey.value,
          token: response.data.data.token,
        });

        setJwtTokenHeader(response.data.data.token);
        router.push({ path: "/" });
      }
    })
    .catch(function (error) {
      console.log(error);
      toast.add({
        severity: "error",
        summary: "Hata!",
        detail: "Oturum açma işlemi esnasında bir hata oluştu!",
        life: 3000,
      });
    });
};
</script>

<template>
  <div
    class="surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden"
  >
    <div class="flex flex-column align-items-center justify-content-center">
      <img
        src="/logo-dark.svg"
        alt="Sakai logo"
        class="mb-5 w-6rem flex-shrink-0"
      />
      <div
        style="
          border-radius: 56px;
          padding: 0.3rem;
          background: linear-gradient(
            180deg,
            var(--primary-color) 10%,
            rgba(33, 150, 243, 0) 30%
          );
        "
      >
        <div
          class="w-full surface-card py-8 px-5 sm:px-8"
          style="border-radius: 53px"
        >
          <div class="text-center mb-5">
            <div class="text-900 text-3xl font-medium mb-3">Hoş geldiniz!</div>
          </div>

          <form role="form" @submit.prevent="onSubmit" class="p-fluid">
            <label for="apiKey" class="block text-900 text-xl font-medium mb-2"
              >Api Key</label
            >
            <InputText
              id="apiKey"
              type="text"
              placeholder="Api Key"
              class="w-full md:w-30rem mb-5"
              style="padding: 1rem"
              v-model="apiKey"
            />

            <label
              for="password1"
              class="block text-900 font-medium text-xl mb-2"
              >Password</label
            >
            <Password
              id="password1"
              v-model="password"
              placeholder="Password"
              :toggleMask="true"
              class="w-full mb-3"
              inputClass="w-full"
              inputStyle="padding:1rem"
            ></Password>

            <Button
              type="submit"
              label="Giriş"
              class="w-full p-3 text-xl"
            ></Button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.pi-eye {
  transform: scale(1.6);
  margin-right: 1rem;
}

.pi-eye-slash {
  transform: scale(1.6);
  margin-right: 1rem;
}
</style>
