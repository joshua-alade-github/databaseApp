// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  runtimeConfig: {
    public: {
      BASE_API_URL: process.env.BASE_API_URL
    },
    private: {
      BASE_API_URL: process.env.BASE_API_URL
    }
  },
  modules: [
    '@pinia/nuxt',
  ],
  css: ['~/assets/scss/main.scss'],
})
