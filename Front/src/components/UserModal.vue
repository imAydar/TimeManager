<template>
    <div>
        <el-dialog title="Пользователь" :visible="visible" width="30%" :before-close="handleClose" @close="onClose">
            <p v-if="this.errors.length > 0" class="errors">
                <b>Пожалуйста исправьте указанные ошибки:</b>
                <ul>
                    <li v-for="error in this.errors" :key="error">{{ error }}</li>
                </ul>
            </p>
            <el-form  label-width="100px" :model="form">
                <el-form-item label="Имя">
                    <el-input v-model="form.firstName"></el-input>
                </el-form-item>
                <el-form-item label="Фамилия">
                    <el-input v-model="form.lastName"></el-input>
                </el-form-item>
                <el-form-item label="Отчество">
                    <el-input v-model="form.middleName"></el-input>
                </el-form-item>
                 <el-form-item label="Email">
                    <el-input v-model="form.email"></el-input>
                </el-form-item>
            </el-form>
            <span slot="footer" class="dialog-footer">
            <el-button @click="handleClose()">Отмена</el-button>
            <el-button type="primary" @click="sendForm()">Ок</el-button>
            </span>
        </el-dialog>
    </div>
</template>
<script>
import { http } from '@/helpers/http-common';

export default {
  data() {
    return {
      errors: []
    }
  },
  props: {
    visible: Boolean,
    handleClose: Function,
    form: Object
  },
  methods: {
    checkInputs() {
      this.errors = [];
      for (var prop in this.form) {
        if (this.form[prop] == "")
          this.form[prop] = null;
      }
      if (!this.validateEmail(this.form.email)) {
        this.errors.push("mail is not valid");
        return false;
      }
      return true;
    },
    validateEmail(email) {
      const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(String(email).toLowerCase());
    },
    sendForm() {
      if (!this.checkInputs())
        return;
      if (this.form.id) { // если есть id значит мы редактируем
        http.put('user',
          JSON.stringify(this.form)
        ).then(() => { //если все хорошо закрываем модалку
          this.handleClose();
        }).catch((error) => {
          let msg = error.response.data;
          this.errors.push(msg);
        });
      } else { // если нет id значит мы создаем
        this.form.id = 0;
        http.post("user",
          JSON.stringify(this.form)
        ).then(() => {
          this.handleClose();
        }).catch((error) => {
          let msg = error.response.data;
          this.errors.push(msg);
        });
      }
    },
    onClose() {
      this.errors = [];
    }
  }
}
</script>

<style scoped>
.errors{
    color: red;
    margin: -20px 0 20px 20px;
}
</style>