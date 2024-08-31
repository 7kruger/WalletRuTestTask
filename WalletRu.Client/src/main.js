import { registerPlugins } from '@/plugins'
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'
import VueDatePicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'

import App from './App.vue'

import { createApp } from 'vue'
import router from '@/router'

const app = createApp(App)

registerPlugins(app)

app.use(router)
app.use(Toast, {
  transition: 'Vue-Toastification__fade',
  maxToasts: 20,
  newestOnTop: true
})

app.component('VueDatePicker', VueDatePicker)

app.mount('#app')
