import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      logger.log(res.data)
      AppState.account = res.data
      return AppState.account
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getAccountKeeps(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.accountKeeps = res.data
    return AppState.accountKeeps
  }

  async getAccountVaults(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    logger.log(res.data)
    AppState.accountVaults = res.data
    return AppState.accountVaults
  }
}

export const accountService = new AccountService()
