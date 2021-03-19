import Vue from 'vue'
import VueRouter from 'vue-router'
import UsersList from '../views/UsersList.vue'
import User from '../views/User.vue'
import NotFound from '../components/NotFound.vue'

Vue.use(VueRouter)

const routes = [{
        path: '/',
        redirect: '/users'
    },
    {
        path: '/users',
        name: 'Users',
        component: UsersList
    },
    {
        path: '/user/:id',
        name: 'User',
        component: User
    },
    {
        path: '/404',
        name: '404',
        component: NotFound,
    },
    {
        path: '*',
        redirect: '/404'
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router