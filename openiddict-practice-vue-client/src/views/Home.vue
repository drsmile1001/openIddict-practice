<template>
  <div class="home">
    <h1>Home</h1>
    <p v-if="userProfile">Logged in! User:{{ userProfile.id }}</p>
    <button v-if="!userProfile" @click="login">Log in</button>
    <button v-if="userProfile" @click="logout">Log out</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { userProfile, login, logout } from "../services/AuthService";

export default defineComponent({
  name: "Home",
  setup() {
    return {
      userProfile,
      login,
      logout,
    };
  },
});
</script>
