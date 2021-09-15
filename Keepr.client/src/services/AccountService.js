import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      logger.log(res.data)
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getAccountKeeps(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.keeps = res.data
    return AppState.keeps
  }

  async getAccountVaults(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.vaults = res.data
    return AppState.vaults
  }
}

export const accountService = new AccountService()
