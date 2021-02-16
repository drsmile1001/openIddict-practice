import { UserManager, WebStorageStateStore, Log, User } from "oidc-client";
import { computed, reactive } from "vue";
Log.logger = console;
Log.level = Log.INFO;

const authority = "https://localhost:5001";

const userManager = new UserManager({
  userStore: new WebStorageStateStore({ store: window.localStorage }),
  authority: authority,
  client_id: "vue-client",
  redirect_uri: "http://localhost:8080/callback.html",
  automaticSilentRenew: true,
  silent_redirect_uri: "http://localhost:8080/silent-renew.html",
  response_type: "code",
  scope: "openid profile",
  post_logout_redirect_uri: "http://localhost:8080",
  filterProtocolClaims: true,
});

const state = reactive({
  user: null as User | null,
});

export const userProfile = computed(() => {
  if (!state.user) return null;

  return {
    id: state.user.profile.sub,
    name: state.user.profile.name,
  };
});

export const accessToken = computed(() => state.user?.access_token);

export async function getUser() {
  const user = await userManager.getUser();
  state.user = user;
  return user;
}

export function login() {
  return userManager.signinRedirect();
}

export function logout() {
  return userManager.signoutRedirect();
}
