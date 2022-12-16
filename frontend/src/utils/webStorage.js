export default function saveToLocalStorage(store) {
  store.subscribe((mutation) => {
    if (mutation.type === "setSignInInfo") {
      const payload = mutation.payload;
      localStorage.setItem("apiKey", payload.apiKey);
    }
    if (mutation.type === "setSignOut") {
      localStorage.removeItem("apiKey");
    }
  });
}
