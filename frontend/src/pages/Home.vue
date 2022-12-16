<script setup>
import { ref, inject, onMounted } from "vue";
import { useToast } from "primevue/usetoast";
const getCryptHub = inject("getCryptHub");
const cryptHub = getCryptHub();

const appAxios = inject("appAxios");
const toast = useToast();
const context = ref("");
const result = ref("");

onMounted(() => {
  cryptHub.client.on("Encrypt", function (input) {
    result.value = input.data.context;

    toast.add({
      severity: "success",
      summary: "Şifrelenmiş data!",
      detail: input.data.context,
      life: 3000,
    });
  });
  cryptHub.client.on("Decrypt", function (input) {
    result.value = input.data.context;
    toast.add({
      severity: "success",
      summary: "Çözümlenmiş data!",
      detail: input.data.context,
      life: 3000,
    });
  });
  cryptHub.start();
});

const encrypt = function () {
  appAxios
    .post("crypt/encrypt", {
      context: context.value,
    })
    .then((response) => {
      toast.add({
        severity: "success",
        summary: "İşlem başarılı!",
        detail: response,
        life: 3000,
      });
    })
    .catch(function (error) {
      console.log(error);
      toast.add({
        severity: "error",
        summary: "Hata!",
        detail: "Şifreleme işlemi esnasında bir hata oluştu!",
        life: 3000,
      });
    });
};

const decrypt = function () {
  appAxios
    .post("crypt/decrypt", {
      context: context.value,
    })
    .then((response) => {
      toast.add({
        severity: "success",
        summary: "İşlem başarılı!",
        detail: response,
        life: 3000,
      });
    })
    .catch(function (error) {
      console.log(error);
      toast.add({
        severity: "error",
        summary: "Hata!",
        detail: "Şifre çözme işlemi esnasında bir hata oluştu!",
        life: 3000,
      });
    });
};
</script>
<template>
  <Card>
    <template #title> Şifreleme / Şifre Çözme </template>
    <template #content>
      <div class="card">
        <div class="formgrid grid">
          <div class="field col">
            <label for="context">İçerik</label>
            <Textarea
              v-model="context"
              rows="3"
              cols="100"
              class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
            />
          </div>
          <div class="field col">
            <label for="result">Sonuç</label>
            <Textarea
              v-model="result"
              rows="3"
              cols="100"
              class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
            />
          </div>
        </div>
      </div>
    </template>
    <template #footer>
      <Button icon="pi pi-send" label="Şifrele" @click="encrypt" />
      <Button
        icon="pi pi-refresh"
        label="Şifreli Metni Çöz"
        class="p-button-warning"
        style="margin-left: 0.5em"
        @click="decrypt"
      />
    </template>
  </Card>
</template>
