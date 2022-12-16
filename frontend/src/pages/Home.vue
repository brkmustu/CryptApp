<script setup>
import { ref, inject } from "vue";
const getCryptHub = inject("getCryptHub");
const cryptHub = getCryptHub();

const context = ref("");
const value = ref("");
</script>
<template>
  <div class="grid">
    <div class="col-2"></div>
    <div class="col-8">
      <Card>
        <template #header>
          <img alt="user header" src="/images/usercard.png" />
        </template>
        <template #title> Şifreleme / Şifre Çözme </template>
        <template #content>
          <div class="grid p-fluid">
            <div class="col-2"></div>
            <div class="col-3">
              <div class="p-inputgroup">
                <Button label="Search" />
                <InputText v-model="context" placeholder="İçerik" />
              </div>
            </div>
            <div class="col-3">
              <InputText v-model="context" placeholder="Sonuç" />
            </div>
            <div class="col-2"></div>
          </div>
        </template>
        <template #footer>
          <Button icon="pi pi-check" label="Save" />
          <Button
            icon="pi pi-times"
            label="Cancel"
            class="p-button-secondary"
            style="margin-left: 0.5em"
          />
        </template>
      </Card>
    </div>
    <div class="col-2"></div>
  </div>
</template>
