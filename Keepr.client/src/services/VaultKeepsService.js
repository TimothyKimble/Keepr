import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Notifier'
import { api } from './AxiosService'

class VaultKeepsService {
  async getKeepsByVaultId(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      logger.log(res.data)
      AppState.vaultKeeps = res.data
      return AppState.vaultKeeps
    } catch (error) {

    }
  }

  async createVaultKeep(newVaultKeep) {
    try {
      const res = await api.post('api/vaultKeeps', newVaultKeep)
      AppState.vaultKeeps.push(res.data)
      return AppState.vaultKeeps
    } catch (error) {
      Pop.toast(error, 'error')
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
