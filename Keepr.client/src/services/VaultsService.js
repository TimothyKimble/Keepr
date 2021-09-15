import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class VaultsService {
  async getVaults(id) {
    try {
      const res = await api.get('api/profile/' + id + '/vaults')
      logger.log(res.data)
      AppState.vaults = res.data
    } catch (error) {
      logger.error("Couldn't get Vaults", error)
    }
  }

  async createVault(newVault) {
    try {
      const res = await api.post('api/vaults', newVault)
      AppState.vaults.push(res.data)
    } catch (error) {
      logger.error("Couldn't Post Vault", error)
    }
  }
}

export const vaultsService = new VaultsService()
