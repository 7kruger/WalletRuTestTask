<template>
  <Header/>
  <main class="d-flex justify-center pa-5 h-100">
    <Suspense>
      <template #default>
        <router-view/>
      </template>
      <template #fallback>
        <VProgressCircular
          indeterminate
          color="primary"
          size="50"
          width="3"
        />
      </template>
    </Suspense>
  </main>
</template>
