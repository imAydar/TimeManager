<template>
    <div>
        <el-dialog title="Пользователь" :visible="visible" width="30%" :before-close="handleClose">
            <p v-if="this.errors" class="errors">
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
            <el-button @click="handleClose()">Cancel</el-button>
            <el-button type="primary" @click="sendForm()">Confirm</el-button>
            </span>
        </el-dialog>
    </div>
</template>
<script>
import axios from 'axios';

export default {
    props: {
        visible: Boolean,
        handleClose: Function,
        form: Object,
        errors: []
    },
    methods: {
        checkInputs() {
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
            this.errors = [];
            if (this.form.id) { // если есть id значит мы редактируем
                axios.put('/api/user',
                    JSON.stringify(this.form), {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }
                ).then(() => { //если все хорошо закрываем модалку
                    this.handleClose();
                }).catch((error) => {
                    let msg = error.response.data
                    this.errors.push(msg);
                });
            } else { // если нет id значит мы создаем
                this.form.id = 0;
                axios.post("/api/user",
                    JSON.stringify(this.form), {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }
                ).then(() => {
                    this.handleClose();
                }).catch((error) => {
                    let msg = error.response.data
                    this.errors.push(msg);
                });
            }
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