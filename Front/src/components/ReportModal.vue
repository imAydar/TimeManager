<template>
   <div>
      <el-dialog title="Отчет" :visible="visible" width="30%" :before-close="handleClose" @close="onClose">
        <p v-if="this.errors.length > 0" class="errors">
            <b>Пожалуйста исправьте указанные ошибки:</b>
            <ul>
            <li v-for="error in errors" :key="error">{{ error }}</li>
            </ul>
        </p>
         <el-form  label-width="100px" :model="form">
            <el-form-item label="Описание" >
               <el-input type="textarea" :rows="5" class="descripton" v-model="form.description"></el-input>
            </el-form-item>
            <el-form-item label="Дата начала">
               <el-date-picker v-model="form.startDate" type="datetime" placeholder="Pick a day" :clearable="false"></el-date-picker>
            </el-form-item>
            <el-form-item label="Часы">
                 <el-input-number v-model="form.hours" :precision="2" :step=0.1 :max=10 :min=0.05 ></el-input-number>
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
      return true;
    },
    sendForm() {
      if (!this.checkInputs())
        return;
      if (this.form.id) { // если есть id значит мы редактируем
        http.put('report',
          JSON.stringify(this.form)
        ).then(() => { //если все хорошо закрываем модалку
          this.handleClose();
        }).catch((error) => {
          let msg = error.response.data
          this.errors.push(msg);
        });
      } else { // если нет id значит мы создаем
        this.form.id = 0;
        http.post("report",
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