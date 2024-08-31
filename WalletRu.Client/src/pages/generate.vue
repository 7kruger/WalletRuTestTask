<script setup>
import {ref} from 'vue'
import {sha256} from '@/utils'
import {sentence} from '@ndaidong/txtgen'
import {useToast} from 'vue-toastification'

const isGenerating = ref(false)
const message = ref('')

const toast = useToast()
let intervalId

async function startMessageGeneration() {
  intervalId = setInterval(async () => {
    message.value = sentence().slice(0, 128)
    await send()
  }, 1000)
}

function stopMessageGeneration() {
  clearInterval(intervalId)
}

function toggleGeneration() {
  if (isGenerating.value) {
    stopMessageGeneration()
  } else {
    startMessageGeneration()
  }
  isGenerating.value = !isGenerating.value
}

async function send() {
  const messageObj = {
    body: message.value,
    serialNumber: await sha256(message.value + Date.now())
  }

  const response = await fetch(`${import.meta.env.VITE_API_URL}/api/messages`, {
    method: 'post',
    headers: {
      'content-type': 'application/json'
    },
    body: JSON.stringify(messageObj)
  })

  if (!response.ok) {
    const result = await response.json()
    showErrorNotification(result.errorMessage ?? 'Error')
  }
}

function showErrorNotification(message) {
  toast.error(message, {
    position: 'top-right',
    timeout: 2000,
    closeOnClick: true,
    pauseOnFocusLoss: true,
    pauseOnHover: true,
    draggable: true,
    draggablePercent: 0.5,
    showCloseButtonOnHover: false,
    hideProgressBar: false,
    closeButton: 'button',
    icon: true
  })
}
</script>

<template>
  <div class="d-flex flex-column align-center justify-start">
    <div v-if="isGenerating" class="mb-2">
      <h3>{{ message }}</h3>
    </div>
    <div v-else class="mb-2">
      <h1>Click "Start" button to start generation messages</h1>
    </div>
    <div>
      <v-btn @click="toggleGeneration">
        {{ isGenerating ? 'Stop' : 'Start' }}
      </v-btn>
    </div>
  </div>
</template>
