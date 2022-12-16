<script setup>
import AppFooter from "./AppFooter.vue";
import AppTopbar from "./AppTopbar.vue";
</script>

<template>
  <div className="grid">
    <div className="col-8 col-offset-2">
      <div className="card">
        <div class="flex flex-column card-container">
          <app-topbar
            class="flex align-items-center justify-content-center"
          ></app-topbar>
          <div class="mt-8 flex align-items-center justify-content-center">
            <router-view></router-view>
          </div>
          <app-footer
            class="absolute bottom-0 flex align-items-center justify-content-center"
            style="left: 45%"
          ></app-footer>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
