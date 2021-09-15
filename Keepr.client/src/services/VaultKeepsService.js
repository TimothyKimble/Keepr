import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { api } from './AxiosService'

class VaultKeepsService {
  async createVaultKeep(newVaultKeep) {
    try {
      const res = await api.post('api/vaultKeeps', newVaultKeep)
      AppState.vaultKeeps.push(res.data)
    } catch (error) {
      Pop.toast(error, 'error')
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
