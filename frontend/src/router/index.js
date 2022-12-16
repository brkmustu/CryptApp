import { createRouter, createWebHashHistory } from "vue-router";
import AppLayout from "@/layout/AppLayout.vue";
import store from "@/store/index.js";

const router = createRouter({
  history: createWebHashHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      component: AppLayout,
      children: [
        {
          path: "/",
          name: "home",
          component: () => import("@/pages/Home.vue"),
        },
      ],
    },
    {
      path: "/pages/notfound",
      name: "notfound",
      component: () => import("@/pages/NotFound.vue"),
    },
    {
      path: "/auth/signin",
      name: "signin",
      component: () => import("@/pages/auth/SignIn.vue"),
    },
    {
      path: "/auth/access",
      name: "accessDenied",
      component: () => import("@/pages/auth/Access.vue"),
    },
    {
      path: "/auth/error",
      name: "error",
      component: () => import("@/pages/auth/Error.vue"),
    },
  ],
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = store.getters.authenticated;

  if (to.name !== "signin" && !isAuthenticated) {
    next({ name: "signin" });
  } else next();
});

export default router;
