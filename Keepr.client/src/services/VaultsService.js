import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Notifier'
import { router } from '../router'

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

  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    logger.log(res.data)
    AppState.activeVault = res.data
    return AppState.activeVault
  }

  async createVault(newVault) {
    try {
      const res = await api.post('api/vaults', newVault)
      AppState.vaults.push(res.data)
      return AppState.vaults
    } catch (error) {
      logger.error("Couldn't Post Vault", error)
    }
  }

  async deleteVault(vaultId) {
    try {
      if (await Pop.confirm()) {
        await api.delete('api/vaults/' + vaultId)
        router.push('/account')
        Pop.toast('Deleted Vault!', 'success')
      }
    } catch (error) {
      logger.error("Couldn't Delete Vault", error)
    }
  }
}

export const vaultsService = new VaultsService()
