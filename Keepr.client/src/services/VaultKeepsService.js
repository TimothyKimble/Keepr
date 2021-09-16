import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Notifier'
import { api } from './AxiosService'
import { keepsService } from './KeepsService'
import { profilesService } from './ProfilesService'

class VaultKeepsService {
  async getKeepsByVaultId(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      AppState.vaultKeeps = res.data
      logger.log(res.data)
      return AppState.vaultKeeps
    } catch (error) {
      logger.log(error)
    }
  }

  async increaseKeeps(keep) {
    try {
      keep.keeps++
      const res = await api.put('api/keeps/' + keep.id, keep)
      logger.log(res.data)
      AppState.activeKeep = res.data
      return AppState.activeKeep
    } catch (error) {
      logger.log(error)
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
