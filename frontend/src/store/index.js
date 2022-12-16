import Vuex from "vuex";

import saveToLocalStorage from "@/utils/webStorage.js";

const store = new Vuex.Store({
  plugins: [saveToLocalStorage],
  state: {
    signInInfo: {
      authenticated: false,
      apiKey: "",
      token: "",
    },
  },
  mutations: {
    setSignOut(state) {
      state.signInInfo = {
        authenticated: false,
        apiKey: "",
        token: "",
      };
    },
    setSignInInfo(state, parameterObj) {
      state.signInInfo = {
        authenticated: true,
        apiKey: parameterObj.apiKey,
        token: parameterObj.token,
      };
    },
  },
  actions: {},
  getters: {
    apiKey(state) {
      return state.signInInfo.apiKey;
    },
    authenticated(state) {
      return state.signInInfo.authenticated;
    },
  },
});

export default store;
