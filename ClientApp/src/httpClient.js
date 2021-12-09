import axios from 'axios'

class httpClient {

    constructor() {
        const token = JSON.parse(localStorage.getItem('user') || '{}')['token']
        const instance = axios.create({
            baseURL: '/',
            headers: { 'Authorization': `Bearer ${token}` }
        })

        this.axiosInstance = instance
    }

    get(url) {
        return this.axiosInstance.get(url)
            .then((resp) => {
            })
            .catch((resp) => {
                if (resp.response !== undefined && resp.response.status == '401') {
                    localStorage.removeItem('user')
                    window.location.replace('/login')
                } else {
                    return Promise.reject(resp)
                }
            })
    }

    post(url, formData) {
        return this.axiosInstance.post(url, formData)
            .then((resp) => {
            })
            .catch((resp) => {
                if (resp.response !== undefined && resp.response.status == '401') {
                    localStorage.removeItem('user')
                    window.location.replace('/login')
                } else {
                    return Promise.reject(resp)
                }
            })
    }

    setTokenOnLogin = () => {
        const token = JSON.parse(localStorage.getItem('user') || '{}')['token']
        this.axiosInstance.defaults.headers = { 'Authorization': `Bearer ${token}` }
    }

    clearTokenOnLogout = () => {
        localStorage.removeItem('user')
        this.axiosInstance.defaults.headers = {}
    }
}

const instance = new httpClient()

export default instance